using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Cw02
{
    class toXML
    {
        public static void Save(List<String> data, String path)
        {
            XElement xml = new XElement("Uczelnia",
                new XAttribute("createdAt", DateTime.Now.ToString("dd/MM/yyyy")),
                new XAttribute("author", "Mateusz Godlewski"),
                from str in data
                let fields = str.Split(",")
                select new XElement("studenci",
                    new XAttribute("indexNumber", "s" + fields[4]),
                    new XElement("fname", fields[0]),
                    new XElement("lname", fields[1]),
                    new XElement("birthdate", new DateTime(Int32.Parse(fields[5].Split("-")[0]), Int32.Parse(fields[5].Split("-")[1]), Int32.Parse(fields[5].Split("-")[2])).ToString("dd.MM.yyyy")),
                    new XElement("e-mails", fields[6]),
                    new XElement("mothersname", fields[7]),
                    new XElement("fathersname", fields[8]),
                    new XElement("studies",
                        new XElement("name", fields[2]),
                        new XElement("mode", fields[3])
                        )
                    ),
                new XElement("activeStudies",
                    from str in data

                    let fields = str.Split(",")
                    group fields by fields[2] into groupped
                    select new XElement("studies",
                        new XAttribute("name", groupped.Key),
                        new XAttribute("numberOfStudents", groupped.Count())
                        )
                   )
                );
                xml.Save(String.Concat(path + "result.xml"));
         }
    }
}
