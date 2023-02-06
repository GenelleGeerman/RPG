using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Status
{
    public interface ISlowable
    {
        public void Slow(bool isSlow);
        public bool IsSlowed();
    }
}
