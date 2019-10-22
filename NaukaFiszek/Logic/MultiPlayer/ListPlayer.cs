using DTO.Enums;
using DTO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Logic.MultiPlayer
{
    public class ListPlayer<T> : IEnumerable<T>
    {
        List<T> users = new List<T>();
        List<(StatusChangedPlayerList Status, T Login)> changes = new List<(StatusChangedPlayerList, T)>();

        public IEnumerator<T> GetEnumerator()
        {
            return users.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return users.GetEnumerator();
        }
        public void Register(T user)
        {
            users.Add(user);
            changes.Add((StatusChangedPlayerList.Register, user));
        }
        public void Unregister(T login)
        {
            users.Remove(login);
            changes.Add((StatusChangedPlayerList.Leave, login));
        }
        public ChangeLog<(StatusChangedPlayerList Status, T Login)> ChangeLogs(int startIndex = 0)
        {
            return new ChangeLog<(StatusChangedPlayerList Status, T Login)>()
            {
                ChangeLogs = changes.Skip(startIndex).ToList(),
                EndIndex = changes.Count
            };
        }
    }
}