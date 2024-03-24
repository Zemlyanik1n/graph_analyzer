using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class AssignmentOperator
    {


        private enum States { S, LS, D, R, RS, F, E };
        public static bool Check(string str, int start, int end, out string message, out int i) // проверка оператора присваивания
        {
            message = "Строка принадлежит языку";
            int position = start;
            States curState = States.S;
            for (i = position; i < str.Length && curState != States.E && curState != States.F; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        if (LeftSide.Check(str, i, str.Length, out message, out i, ref Analyzer.IdensVar,
                            ref Analyzer.Ids, ref Analyzer.IdensIndex, ref Analyzer.Cnts, ref Analyzer.IdensMassive)) // проверка левой стороны,передачи по ссылке можно заменить на обращения к паблик полям класса
               
                        {
                            curState = States.LS;
                        }
                        else
                        {
                            curState = States.E;
                        }
                        break;
                    case States.LS:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.LS;
                                break;
                            case ':': // проверка :=
                                curState = States.D;
                                break;
                            default:
                                curState = States.E;
                                message = "ОШИБКА! Ожидается пробел или :";
                                break;
                        }
                        break;
                    case States.D:
                        if (curChar == '=') { curState = States.R; }
                        else { curState = States.E; message = "ОШИБКА! Ожидается ="; }
                        break;
                    case States.R:
                        switch (curChar)
                        {
                            case ' ':
                                curState = States.R;
                                break;
                            default:
                                if (RightSide.Check(str, i, str.Length, out message, out i, ref Analyzer.Ids, 
                                    ref Analyzer.Cnts,ref Analyzer.IdensIndex,ref Analyzer.IdensMassive,ref Analyzer.IdensVar)) // проверка правой стороны
                                {
                                    curState = States.F;
                                }
                                else 
                                {
                                    curState = States.E;
                                }
                                break;
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
