using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class Operator
    {
        private enum States { S, F, I, CA, SK, ID, M , E };
        public static bool Check(string str, int start, int end, out string message, out int i)
        {
            bool ide = false;
            bool co = false;
            message = "Строка принадлежит языку";
            int position = start;
            int id1 = start, id2 = start;
            var curIden = new StringBuilder();
            States curState = States.S;
            for (i = position; i < end && curState != States.E && curState != States.F; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        if (curChar == ' ') { curState = States.S; }
                        if ("_".Contains(curChar) || char.IsLetterOrDigit(curChar))
                        {
                            id1 = i;
                            if ((curChar == '_' || char.IsLetter(curChar))
                            && Identifier.Check(str, i, str.Length, out message, out i)) // проверка идентификатора
                            {
                                id2 = i;
                                curState = States.I;
                            }
                            else if ((char.IsDigit(curChar) || curChar == '+' || curChar == '-')
                                && ConstAny.Check(str, i, str.Length, out message, out i)) // проверка любой константы
                            {
                                id2 = i;
                                curState = States.CA;
                            }
                            else
                            {
                                curState = States.E;
                            }
                        }
                        else
                        {
                            message = "ОШИБКА! Ожидается _ или буква или цифра или пробел или ;";
                            curState = States.E;
                        }
                        break;
                    case States.CA:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.CA;
                                break;
                            default:
                                curIden.Append(str, id1, id2 - id1 + 1);

                                if (Analyzer.Cnts.Contains(curIden.ToString()))
                                {
                                    bool flag = false;
                                    List<int> a = new List<int>();
                                    for (int k = 0; k < Analyzer.Cnts.Count; k++)
                                    {
                                        if (Analyzer.Cnts[k] == curIden.ToString())
                                        {
                                            if (Analyzer.CntsView[k] == "константа выражения")
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (!flag)
                                    {
                                        Analyzer.Cnts.Add(curIden.ToString());
                                        Analyzer.CntsView.Add("константа выражения");
                                        if (curIden.ToString().Contains('E'))
                                        {
                                            Analyzer.CntsType.Add("с плавающей точкой");
                                        }
                                        else if (curIden.ToString().Contains('.'))
                                        {
                                            Analyzer.CntsType.Add("с фиксированной точкой");
                                        }
                                        else
                                        {
                                            Analyzer.CntsType.Add("любая константа");
                                        }
                                    }
                                }
                                else
                                {
                                    Analyzer.Cnts.Add(curIden.ToString());
                                    Analyzer.CntsView.Add("константа выражения");
                                    if (curIden.ToString().Contains('E'))
                                    {
                                        Analyzer.CntsType.Add("с плавающей точкой");
                                    }
                                    else if (curIden.ToString().Contains('.'))
                                    {
                                        Analyzer.CntsType.Add("с фиксированной точкой");
                                    }
                                    else
                                    {
                                        Analyzer.CntsType.Add("целая константа");
                                    }
                                }
                                curIden.Clear();
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;
                    case States.I:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.I;
                                break;
                            case '[':

                                curIden.Append(str, id1, id2 - id1 + 1);
                                if (Analyzer.IdensVar.ContainsKey(curIden.ToString()))
                                {
                                    i = id1;
                                    message = "Ошибка! использование переменной в качестве массива";
                                    curState = States.E;
                                    break;
                                }
                                else if (Analyzer.IdensIndex.ContainsKey(curIden.ToString()))
                                {
                                    i = id1;
                                    message = "Ошибка! использование индекса в качестве указателя на массив";
                                    curState = States.E;
                                    break;
                                }
                                else if (!Analyzer.IdensMassive.ContainsKey(curIden.ToString()))
                                {
                                    Analyzer.IdensMassive[curIden.ToString()] = "массив";
                                    Analyzer.Ids.Add(curIden.ToString());
                                    Analyzer.IdsType.Add("массив");
                                }
                                curIden.Clear();
                                curState = States.SK; break;
                            default:
                                curIden.Append(str, id1, id2 - id1 + 1);

                                if (Analyzer.IdensMassive.ContainsKey(curIden.ToString()))
                                {
                                    i = id1;
                                    message = "Ошибка! использование указателя на массив в качестве переменной";
                                    curState = States.E;
                                    break;
                                }
                                //else if (Analyzer.IdensIndex.ContainsKey(curIden.ToString()))
                                //{
                                //    i = id1;
                                //    message = "Ошибка! использование индекса в качестве переменной";
                                //    curState = States.E;
                                //    break;
                                //}
                                else if(!Analyzer.IdensVar.ContainsKey(curIden.ToString()))
                                {
                                    Analyzer.IdensVar[curIden.ToString()] = "переменная";
                                    Analyzer.Ids.Add(curIden.ToString());
                                    Analyzer.IdsType.Add("переменная");
                                }
                                curIden.Clear();
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;
                    case States.SK:
                        if (curChar == ' ') { curState = States.SK; }
                        else if ("_".Contains(curChar) || char.IsLetterOrDigit(curChar) || curChar == '+' || curChar == '-')
                        {
                            id1 = i;
                            if ((curChar == '_' || char.IsLetter(curChar))
                            && Identifier.Check(str, i, str.Length, out message, out i))
                            {
                                curState = States.ID;
                                id2 = i + 1; // mb
                                ide = true;
                            }
                            else if ((char.IsDigit(curChar) || curChar == '+' || curChar == '-')
                                && ConstInt.Check(str, i, str.Length, out message, out i))
                            {
                                curState = States.ID;
                                id2 = i + 1; // mb
                                co= true;   
                            }
                            else
                            {
                                curState = States.E;
                            }
                        }
                        else
                        {
                            message = "ОШИБКА! Ожидается _ или буква или цифра или пробел";
                            curState = States.E;
                        }
                        break;
                    case States.ID:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.ID;
                                break;
                            case ']':
                                if (ide)
                                {
                                    curIden.Append(str, id1, id2 - id1);

                                    if (Analyzer.IdensMassive.ContainsKey(curIden.ToString()))
                                    {
                                        i = id1;
                                        message = "Ошибка! использование указателя на массив в качестве индекса";
                                        curState = States.E;
                                        break;
                                    }
                                    //else if (Analyzer.IdensVar.ContainsKey(curIden.ToString()))
                                    //{
                                    //    i = id1;
                                    //    message = "Ошибка! использование переменной в качестве индекса";
                                    //    curState = States.E;
                                    //    break;
                                    //}
                                    else if (!Analyzer.IdensIndex.ContainsKey(curIden.ToString()))
                                    {
                                        Analyzer.IdensIndex[curIden.ToString()] = "индекс";
                                        Analyzer.Ids.Add(curIden.ToString());
                                        Analyzer.IdsType.Add("индекс");
                                    }
                                    curIden.Clear();
                                }
                                else if (co) 
                                {
                                    curIden.Append(str, id1, id2 - id1);
                                    if (Analyzer.Cnts.Contains(curIden.ToString()))
                                    {
                                        bool flag = false;
                                        List<int> a = new List<int>();
                                        for (int k = 0; k < Analyzer.Cnts.Count; k++)
                                        {
                                            if (Analyzer.Cnts[k] == curIden.ToString())
                                            {
                                                if (Analyzer.CntsView[k] == "индекс")
                                                {
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (!flag)
                                        {
                                            Analyzer.Cnts.Add(curIden.ToString());
                                            Analyzer.CntsView.Add("индекс");
                                            Analyzer.CntsType.Add("константа целая");
                                        }
                                    }
                                    else
                                    {
                                        Analyzer.Cnts.Add(curIden.ToString());
                                        Analyzer.CntsView.Add("индекс");
                                        Analyzer.CntsType.Add("константа целая");
                                    }
                                }




                                curState = States.M;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел или ]";
                                break;
                        }
                        break;
                    case States.M:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.M;
                                break;
                            default:
                                curState = States.F;
                                i--;
                                break;
                        }
                        break;
                }
            }
            i--;
            if(curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            return curState == States.F;
        }
    }
}
