alter procedure PromoteStudents @Studies nvarchar(100), @semester int
as
begin 
	set xact_abort on;
	begin tran
	declare @idStudy int = (select IdStudy from Studies where Name=@Studies);
	declare @idEnrollment int = (select idEnrollment from Enrollment where IdStudy=@idStudy and Semester=@semester );
	if @idStudy is null or @idEnrollment is null
	begin
		rollback;
		RAISERROR ( 'Not Found',1,1);
		return;
	end;
	declare @idNextEnrollment int = (select idEnrollment from Enrollment where 
	IdStudy=@idStudy and Semester=@semester+1 );
	declare @today Date = getDate();
	if @idNextEnrollment is null
	begin
		set @idNextEnrollment = (select max(idEnrollment) from Enrollment)+1;
		insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) 
		values (@idNextEnrollment,@semester+1,@idStudy,@today);
	end;
	update Student set IdEnrollment = @idNextEnrollment where idEnrollment=@idEnrollment;
	commit tran
	return select * from Enrollment where IdEnrollment=@idNextEnrollment;
end;