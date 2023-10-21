using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab.models
{
    public class LinesOptionList
    {
        public List<OptionTypeEnum> linesOption = new List<OptionTypeEnum>();

        public LinesOptionList(int count)
        {
            this.linesOption = new List<OptionTypeEnum>();

            for (int i = 0; i < count; i++)
                this.linesOption.Add(OptionTypeEnum.Nothing);
        }

        public void Add()
        {
            this.linesOption.Add(OptionTypeEnum.Nothing);
        }

        public void RemoveAt(int index)
        {
            if (index - 1 >= 0)
            {
                this.linesOption.RemoveAt(index - 1);
                this.linesOption.RemoveAt(index - 1);
                this.linesOption.Insert(index - 1, OptionTypeEnum.Nothing);
            }
            else
            {
                this.linesOption.RemoveAt(this.linesOption.Count - 1);
                this.linesOption.RemoveAt(0);
                this.linesOption.Add(OptionTypeEnum.Nothing);
            }
        }

        public void Insert(int index)
        {
            this.linesOption.RemoveAt(index - 1);
            this.linesOption.Insert(2, OptionTypeEnum.Nothing);
            this.linesOption.Insert(3, OptionTypeEnum.Nothing);
        }

        public void ChangeOption(int index, OptionTypeEnum option)
        {
            // dokoncz
            this.linesOption[index] = option;
        }

        public (bool, bool) WhichOptionAvailable(int index)
        {
            bool vertical = true;
            bool horizontal = true;

            if (this[index - 1] == OptionTypeEnum.Vertical || this[(index + 1) % this.linesOption.Count] == OptionTypeEnum.Vertical)
                vertical = false;

            if (this[index - 1] == OptionTypeEnum.Horizontal || this[(index + 1) % this.linesOption.Count] == OptionTypeEnum.Horizontal)
                horizontal = false;

            return (vertical, horizontal);
        }

        public OptionTypeEnum this[int index]
        {
            get
            {
                if (index >= 0 && index < linesOption.Count())
                {
                    return this.linesOption[index];
                }
                else
                {
                    if (index < 0 && Math.Abs(index) <= linesOption.Count)
                    {
                        return this.linesOption[this.linesOption.Count() + index];
                    }

                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            set
            {
                if (index >= 0 && index < linesOption.Count())
                {
                    linesOption[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
        }
    }
}
