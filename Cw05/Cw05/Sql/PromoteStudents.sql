create procedure PromoteStudents @Studies nvarchar(100), @Semester int
as
begin 
	set xact_abort on;
	begin tran

	declare @idStudy int = (select IdStudy from Studies where Name=@Studies);
	declare @idEnrollment int = (select idEnrollment from Enrollment where IdStudy=@idStudy and Semester=@Semester );
	if @idStudy is null or @idEnrollment is null
	begin
		RAISERROR ( 'Not Found',1,1);
		return;
	end;
	declare @idNextEnrollment int = (select idEnrollment from Enrollment where 
	IdStudy=@idStudy and Semester=@Semester+1 );

	if @idNextEnrollment is null
	begin
		set @idNextEnrollment = (select max(idEnrollment) from Enrollment)+1;
		insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) 
		values (@idNextEnrollment,@semester+1,@idStudy,getDate());
	end;
	update Student set IdEnrollment = @idNextEnrollment where idEnrollment=@idEnrollment;

	commit
end;
