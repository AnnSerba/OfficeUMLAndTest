﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан инструментальным средством
//     В случае повторного создания кода изменения, внесенные в этот файл, будут потеряны.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class Schedule
{
    private List<Task> listTask;
    Register register;
    string nameFile;
    public virtual List<Task> ListTask
    {
        get
        {
            return listTask;
        }
        set
        {
            listTask = value;
        }
    }
    public Schedule(Register _register, string _nameFile)
    {
        ListTask = new List<Task>();
        register = _register;
        nameFile = _nameFile + ".txt";
        //ReaderFile(nameFile);
        for (int i = 0; i < 1000; i++)
        {
            listTask.Add(new Task());
            listTask.Add(new Task());
        }
    }
    public virtual string ReaderFile(string _nameFile)
    {
        FileInfo file = new FileInfo(_nameFile);
        if (file.Exists == false)
        {
            file.Create();
            return "Файл создан";
        }
        else
        {
            StreamReader streamReader = File.OpenText(_nameFile);
            string line = "";
            while (!streamReader.EndOfStream)
            {
                line += streamReader.ReadLine();
                char[] separator = new char[1];
                separator[0] = ' ';
                string[] stringValue = line.Split(separator);

                Time time = new Time(int.Parse(stringValue[0]), int.Parse(stringValue[1]));
                Date date = new Date(int.Parse(stringValue[2]), int.Parse(stringValue[3]), int.Parse(stringValue[4]));
                Task task = new Task();
                int i = 5;
                while (i < stringValue.Length)
                {
                    task.AddListClients(int.Parse(stringValue[i]), register.ListClients[int.Parse(stringValue[i])]);
                    i++;
                }
                ListTask.Add(task);
            }
            streamReader.Close();
        }
        return "Файл считан";
    }
    public virtual string WriterInFile()
    {
        StreamWriter streamWriter = new StreamWriter(nameFile);
        int i = 0;
        while (i < ListTask.Count)
        {
            streamWriter.Write(ListTask[i].Time.Hour.ToString() + " " + ListTask[i].Time.Minute.ToString() + " "
                + ListTask[i].Date.Day.ToString() + " " + ListTask[i].Date.Month.ToString() + " " + ListTask[i].Date.Year);
            foreach (int j in ListTask[i].ListClients.Keys)
            {
                streamWriter.WriteLine(" " + j);
            }
            i++;
        }
        streamWriter.Close();
        return "Файл записан";
    }
    public virtual string ToString(List<Task> listTask)
    {
        string line = "--------------------------\n";
        for (int i = 0; i < listTask.Count; i++)
        {
            line += listTask[i].ToString();
        }
        line += "\n-------------------------\n";

        return line;
    }
    public virtual string ToString(Task task)
    {
        return task.ToString();
    }
    public virtual string AddTaskToListTask(Time time, Date date)
    {
        if (SearchTaskToListTask(time, date) == null)
        {
            listTask.Add(new Task());
            listTask[listTask.Count - 1].Change(time.Hour, time.Minute, date.Day, date.Month, date.Year);
            return "Задача на " + date.ToString() + " в " + time.ToString() + " успешно добавлена";
        }
        return date.ToString() + " в " + time.ToString() + " назначена задача. Ошибка добавлении задачи";
    }
    public virtual bool AddTaskToListTask(Task task)
    {
        if (SearchTaskToListTask(task.Time, task.Date) == null)
        {
            listTask.Add(new Task());
            if (listTask[listTask.Count - 1].Change(task.Time.Hour, task.Time.Minute,
                task.Date.Day, task.Date.Month, task.Date.Year).CompareTo("Время успешно изменено Дата успешно изменена ") == 0)
            {
                return true;
            }
            listTask.Remove(listTask[listTask.Count - 1]);
        }
        return false;
    }
    public virtual Person SearchClientsToTask(Task task, int numberClient)
    {
        foreach (int i in task.ListClients.Keys)
        {
            if (i == numberClient)
            {
                return task.ListClients[i];
            }
        }
        return null;
    }
    public virtual Task SearchTaskToListTask(Time time, Date date)
    {
        for (int i = 0; i < listTask.Count; i++)
        {
            if (listTask[i].Time == time && listTask[i].Date == date)
            {
                return listTask[i];
            }
        }
        return null;
    }

    public virtual List<Task> SearchTaskToListTask(Time time)
    {
        List<Task> search = new List<Task>();
        for (int i = 0; i < listTask.Count; i++)
        {
            if (listTask[i].Time == time)
            {
                search.Add(listTask[i]);
            }
        }
        return search;
    }

    public virtual List<Task> SearchTaskToListTask(Date date)
    {
        List<Task> search = new List<Task>();
        for (int i = 0; i < listTask.Count; i++)
        {
            if (listTask[i].Date == date)
            {
                search.Add(listTask[i]);
            }
        }
        return search;
    }

    public virtual string DeleteTaskToListTask(Time time, Date date)
    {
        if (SearchTaskToListTask(time, date) != null)
        {
            listTask.Remove(SearchTaskToListTask(time, date));
            return date.ToString() + " в " + time.ToString() + " задача удалена";
        }
        return date.ToString() + " в " + time.ToString() + " задача не назначена. Ошибка удаления";
    }

    public virtual bool ChangeTaskToListTask(Time time, Date date, Task task)
    {
        task.Change(time.Hour, time.Minute, date.Day, date.Month, date.Year);
        if (task.Time.StatusError(time.Hour, time.Minute) == "" &&
            task.Date.StatusError(date.Day, date.Month, date.Year) == "")
        {
            return true;
        }
        return false;
    }

    public virtual string AddClientsToTask(Task task, int numberClient)
    {
        if (SearchClientsToTask(task, numberClient) == null)
        {
            task.ListClients.Add(numberClient, register.ListClients[numberClient]);
            return "Клиент на " + task.Date.ToString() + " в " + task.Time.ToString() + "с номером" + numberClient + " успешно добавлен";
        }
        return task.Date.ToString() + " в " + task.Time.ToString() + "Клиент с номером " + numberClient + " уже добавленю. Ошибка добавления клиента";
    }

    public virtual string DeleteClientsToTask(Task task, int numberClient)
    {
        if (SearchClientsToTask(task, numberClient) != null)
        {
            task.ListClients.RemoveAt(numberClient);
            return "Клиент на " + task.Date.ToString() + " в " + task.Time.ToString() + " с номером" + numberClient + " успешно удалён";
        }
        return task.Date.ToString() + " в " + task.Time.ToString() + "Клиент с номером " + numberClient + " не существует";
    }

}
