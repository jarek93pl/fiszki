using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class TeachSetFiche : BaseConector
    {
        public void Add(DTO.Models.TeachSetFiche teachSetFiche)
        {
            teachSetFiche.IdSetFiche = LoadInt("AddTeachSetFiche", new Dictionary<string, object>()
            {
                {"Name",teachSetFiche.Name},
                {"IdSetFiche", teachSetFiche.IdSetFiche },
                {"FirstTypeAnswear", teachSetFiche.FirstTypeAnswear }
            });
            using (DataTable data = LoadTeachBag(teachSetFiche.teachBags))
            {
                NonReturned("AddTeachBags", new Dictionary<string, object>()
                {
                    {"IdTeachFiche",teachSetFiche.IdSetFiche},
                    {"TeachBags",  data}
                });
            }
        }
        public void Delete(int id)
        {
            NonReturned("DeleteTeachSet", new Dictionary<string, object>()
            {
                { "Id",id }
            });
        }
        public List<DTO.Models.TeachSetFiche> SearchTeachSetsByUser(int userId)
        {
            return LoadList<DTO.Models.TeachSetFiche>("SearchTeachSetsFiche",
                new Dictionary<string, object>()
                {
                    { "UserId",userId }
                }
                , Reader
                );
        }
        private DataTable LoadTeachBag(IEnumerable<DTO.Models.TeachBag> teachBags)
        {
            DataTable data = new DataTable();
            data.Columns.Add("TypeAnswear", typeof(int));
            data.Columns.Add("PeriodTime", typeof(TimeSpan));
            data.Columns.Add("LimitTimeSek", typeof(int));
            data.Columns.Add("Number", typeof(int));
            int CurrentNumber = 0;
            foreach (var item in teachBags)
            {
                data.Rows.Add(item.TypeAnswear, item.PeriodTime, item.LimitTimeInSek, CurrentNumber++);
            }
            return data;
        }

        private DTO.Models.TeachSetFiche Reader(Loader arg)
        {
            return new DTO.Models.TeachSetFiche()
            {
                IdSetFiche = arg.GetInt("IdSet"),
                Id = arg.GetInt("Id"),
                Name = arg.GetString("Name"),
                NameSet = arg.GetString("NameSet"),
                DataCreated = arg.GetDate("DateCreated")
            };
        }
    }
}
