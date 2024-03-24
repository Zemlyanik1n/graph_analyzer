using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer
{
    static class Identifier
    {
        private enum States { S, A, F, E };
        public static bool Check(string str, int start, int end, out string message, out int i) // проверка идентификатора
        {
            int st = start;
            var names = new string[]{ "WITH", "DO", "DIV", "MOD" }; // список резервированных слов
            int curLength = 0;
            message = "Cтрока принадлежит языку";
            
            States curState = States.S;
            for (i = start; i < end && curState != States.F && curState != States.E; i++)
            {
                var curChar = str[i];
                switch (curState)
                {
                    case States.S:
                        switch (curChar)
                        {
                            case '_':
                                curState = States.A;
                                curLength++;
                                break;
                            default:
                                if (char.IsLetter(curChar))
                                {
                                    curState = States.A;
                                    curLength++;
                                }
                                else
                                {
                                    curState = States.E;
                                    message = "ОШИБКА! Ожидается _ или буква!";
                                }
                                break;
                        }
                        break;
                    case States.A:
                        if (char.IsLetterOrDigit(curChar) && curLength == 8)
                        {
                            curState = States.E;
                            message = "ОШИБКА! Длина идентификатора не должна быть больше 8 символов";
                        }   
                        else if (char.IsLetterOrDigit(curChar))
                        {
                            curState = States.A;
                            curLength++;
                        }
                        else
                        {
                            curState = States.F;
                            i--;
                        }
                        break; // опасное место
                }
            }
            i--;
            if (curState != States.F && curState != States.E)
            {
                i++;
                message = "Ошибка! Ожидалось продолжение!";
            }
            else if (curState == States.F && names.Contains(str.Substring(st, i - st + 1)))
            {
                i = st;
                message = "ОШИБКА! Использовано зарезервированное имя div mod do with!";
                curState = States.E;
            }
            return curState == States.F;
        }
    }
}
