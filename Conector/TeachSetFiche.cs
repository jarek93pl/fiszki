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
        public void Add(DTO.TeachSetFiche teachSetFiche)
        {
            teachSetFiche.IdSetFiche = LoadInt("AddTeachSetFiche", new Dictionary<string, object>()
            {
                {"Name",teachSetFiche.Name},
                {"IdSetFiche", teachSetFiche.IdSetFiche }
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
        public List<DTO.TeachSetFiche> SearchTeachSetsByUser(int userId)
        {
            return LoadList<DTO.TeachSetFiche>("SearchTeachSetsFiche",
                new Dictionary<string, object>()
                {
                    { "UserId",userId }
                }
                ,Reader
                );
        }
        private DataTable LoadTeachBag(IEnumerable<DTO.TeachBag> teachBags)
        {
            DataTable data = new DataTable();
            data.Columns.Add("TypeAnswear", typeof(int));
            data.Columns.Add("PeriodTime", typeof(TimeSpan));
            data.Columns.Add("LimitTimeSek", typeof(int));
            foreach (var item in teachBags)
            {
                data.Rows.Add(item.TypeAnswear, item.PeriodTime, item.LimitTimeInSek);
            }
            return data;
        }

        private DTO.TeachSetFiche Reader(Loader arg)
        {
            return new DTO.TeachSetFiche()
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
