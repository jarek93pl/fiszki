﻿using DTO.Models;
using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conector
{
    public class Fiche : BaseConector
    {
        public void DeleteFiche(int id)
        {
            NonReturned("DeleteFiche", new Dictionary<string, object>() { { "Id", id } });
        }
        public List<DTO.Models.Fiche> SearchFiches(int SetFicheId)
        {
            return LoadList<DTO.Models.Fiche>("SearchFiches", new Dictionary<string, object>() { { "@SetFicheId", SetFicheId.ToString() } }, Reader);
        }
        public DTO.Models.Fiche LoadFiche(int FicheId)
        {
            DTO.Models.Fiche fiche = LoadList<DTO.Models.Fiche>("SearchFiches", new Dictionary<string, object>() { { "@FicheId", FicheId.ToString() } }, Reader).First();
            fiche.FicheResponses = LoadFicheResponse(FicheId);
            return fiche;
        }
        public FicheResponse[] LoadFicheResponse(int FicheId)
        {
            return LoadList<FicheResponse>("SearchResponse", new Dictionary<string, object>() { { "IdFiche", FicheId } }, ReaderFicheResponse).ToArray();
        }

        private FicheResponse ReaderFicheResponse(Loader arg)
        {//[fr].Id,[fr].[IdFile],[fr].[Name],[fr].[TypePrompt]
            return new FicheResponse()
            {
                Id = arg.GetInt("Id"),
                IdFile = arg.GetIntNullable("IdFile"),
                Name = arg.GetString("Name"),
                TypePrompt = (ContentType)arg.GetInt("TypePrompt"),
                IsCorect = arg.GetBoolen("IsCorect")

            };
        }

        public void SendFiche(DTO.Models.Fiche sendFiche)
        {
            sendFiche.Id = LoadInt("SaveFiche", new Dictionary<string, object>() {
                { "Prompt" ,sendFiche.Prompt },
                { "Response" ,sendFiche.Response },
                { "TypePrompt" ,sendFiche.TypePrompt },
                { "Id" ,sendFiche.Id },
                { "IdPromptFile" ,sendFiche.IdPromptFile==0?null:sendFiche.IdPromptFile},
                { "IdFicheSet" ,sendFiche.IdFicheSet }
            });

            using (DataTable dataTable = CreateResponseTable(sendFiche.FicheResponses))
            {
                sendFiche.Id = LoadInt("MergeFicheResponses", new Dictionary<string, object>() {
                    {"TableFicheResponse", dataTable },
                    { "IDFiche",sendFiche.Id}
                });
            }
        }

        private DataTable CreateResponseTable(FicheResponse[] fiches)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("IdFile", typeof(int));
            dataTable.Columns.Add("TypePrompt", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("IsCorect", typeof(string));
            if (fiches != null)
            {
                foreach (var item in fiches)
                {
                    dataTable.Rows.Add(item.Id, item.IdFile == 0 ? null : item.IdFile, item.TypePrompt, item.Name, item.IsCorect);
                }
            }
            return dataTable;
        }
        public void DeleteResponse(int id)
        {
            LoadInt("DeleteResponse", new Dictionary<string, object>() { { "id", id } });
        }
        private DTO.Models.Fiche Reader(Loader arg)
        {
            DTO.Models.Fiche returned = new DTO.Models.Fiche();
            returned.Prompt = arg.GetString("Prompt");
            returned.Response = arg.GetString("Response");
            returned.NameTypePrompt = arg.GetString("NameTypePrompt");
            returned.Id = arg.GetInt("Id");
            returned.IdFicheSet = arg.GetInt("IdSetFiche");
            returned.IdPromptFile = arg.GetIntNullable("IdFile");
            returned.TypePrompt = (ContentType)arg.GetInt("TypePrompt");
            return returned;
        }
    }
}
