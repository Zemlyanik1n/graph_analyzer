using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class RightSide
    {
        private enum States { S, O, O1, O2, F, E };
        public static bool Check(string str, int start, int end, out string message, out int i,
            ref List<string> Idens, ref List<string> consts, ref Dictionary<string, string> Index, ref Dictionary<string, string> Massive,
            ref Dictionary<string, string> Vars) // проверка правой стороны, ссылки можно заменить на обращенигя к статик полям
        {
            message = "Строка принадлежит языку";
            States curState = States.S;
            int position = start;
            for (i = position; i < end && curState != States.E && curState != States.F; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        if (Operator.Check(str, i, str.Length, out message, out i))
                        {
                            curState = States.O;
                            break;
                        }
                        else
                        {
                            curState = States.E; // опасное место
                            break;
                        }
                    case States.O:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.O;
                                break;
                            default:
                                if (("*-+/".ToArray().Contains(curChar) || curChar == 'D' || curChar == 'M'))
                                {
                                    if (Operation.Check(str, i, str.Length, out message, out i))
                                        curState = States.O1;
                                    else
                                        curState = States.E;
                                }
                                else
                                {
                                    curState = States.F;
                                    i--;
                                }
                                break; // opasno
                        }
                        break;
                    case States.O1:
                        if ((str[i - 1] == 'D' || str[i - 1] == 'V') && curChar != ' ')
                        {
                            message = "ОШИБКА! Ожидается пробел после операции";
                            curState = States.E;
                        }
                        else if (curChar == ' ')
                        {
                            curState = States.O1;
                        }
                        else if (Operator.Check(str, i, str.Length, out message, out i))
                        {
                            curState = States.O2;
                        }
                        else
                        {
                            curState = States.E;
                        }
                        break;
                    case States.O2:
                        if (curChar == ' ')
                        {
                            curState = States.O2;
                        }
                        else
                        {
                            i--;
                            curState = States.F;
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
