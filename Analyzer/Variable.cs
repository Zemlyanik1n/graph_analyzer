using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class Variable
    {
        private enum States { S, I, I0, I1, F, E };  // проверка переменной
        public static bool Check(string str, int start, int end, out string message, out int i, ref Dictionary<string, string> Idens, ref List<string> Ids)
        {
            StringBuilder curIden = new StringBuilder();
            int id1 = 0;
            int id2 = 0;
            message = "Строка принадлежит языку";
            States curState = States.S;
            for (i = start; i < end && curState != States.F && curState != States.E; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.S;
                                break;
                            default:
                                id1 = i;
                                if (Identifier.Check(str, i, str.Length, out message, out i))
                                {
                                    curState = States.I;
                                }
                                else
                                {
                                    curState = States.E;
                                }
                                break;
                        }
                        break;
                    case States.I:
                        switch (curChar)
                        {
                            case ',':
                                id2 = i;
                                curIden.Append(str, id1, id2 - id1);
                                if (Idens.ContainsKey(curIden.ToString()))
                                {
                                    message = "Ошибка! Повторное использование перменной для присоединения!";
                                    i = id1;
                                    curState = States.E;
                                    break;
                                }
                                Idens[curIden.ToString()] = "Переменная для присоединения";
                                Ids.Add(curIden.ToString());
                                Analyzer.IdsType.Add("Переменная для присоединения");
                                curIden.Clear();
                                curState = States.S;
                                break;
                            case '.':
                                curState = States.I0;
                                break;
                            default:
                                id2 = i;
                                curIden.Append(str, id1, id2 - id1);
                                if (Idens.ContainsKey(curIden.ToString()))
                                {
                                    message = "Ошибка! Повторное использование перменной для присоединения!";
                                    i = id1;
                                    curState = States.E;
                                    break;
                                }
                                Idens[curIden.ToString()] = "Переменная для присоединения";
                                Ids.Add(curIden.ToString());
                                Analyzer.IdsType.Add("Переменная для присоединения");
                                curIden.Clear();
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;
                    case States.I0:
                        if (Identifier.Check(str, i, str.Length, out message, out i))
                        {
                            curState = States.I1;
               
                        }
                        else { curState = States.E;}
                        break;
                    case States.I1:
                        id2 = i;
                        curIden.Append(str, id1, id2 - id1);
                        if (Idens.ContainsKey(curIden.ToString()))
                        {
                            message = "Ошибка! Повторное использование перменной для присоединения c полем!";
                            i = id1;
                            curState = States.E;
                            break;
                        }
                        Idens[curIden.ToString()] = "Переменная для присоединения с полем";
                        Ids.Add(curIden.ToString());
                        Analyzer.IdsType.Add("Переменная для присоединения с полем");
                        curIden.Clear();
                        if (curChar == ',')
                        {
                            curState = States.S;
                        }
                        else
                        {
                            curState = States.F;
                            i--;
                        }
                        break;
                }
            }
            i--;
            if (curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            return curState == States.F;
        }
    }
}
