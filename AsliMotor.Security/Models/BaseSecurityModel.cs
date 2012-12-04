using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BonaStoco.Inf.Data.ViewModel;

namespace AsliMotor.Security.Models
{
    public class BaseSecurityModel:IViewModel
    {
        string _name;

        public BaseSecurityModel() { }
        public BaseSecurityModel(Guid id, string name, string applicationName)
        {
            Id = id;
            _name = name;
            ApplicationName = applicationName;
        }
        public Guid Id { get; set; }
        public virtual string Name
        {
            get { return _name; }
            set
            {
                AssertValidName(value);
                _name = value;
            }
        }
        public virtual string ApplicationName { get; set; }

        public override bool Equals(object obj)
        {
            BaseSecurityModel model = obj as BaseSecurityModel;
            if (model.IsNull()) return false;
            return model.Key.Equals(Key);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
        public object Key { get; set; }
        public override string ToString()
        {
            return String.Format("{0} at ({1})", Name, ApplicationName);
        }

        protected virtual void AssertValidName(string name)
        {
            StringBuilder invalidChar = new StringBuilder();
            if (name.Contains(","))
                invalidChar.Append("','");
            if (name.Contains(" "))
            {
                if (invalidChar.Length > 0) invalidChar.Append(",");
                invalidChar.Append("' '");
            }
            if (name.Contains("*"))
            {
                if (invalidChar.Length > 0) invalidChar.Append(",");
                invalidChar.Append("'*'");
            }
            if (name.Contains("?"))
            {
                if (invalidChar.Length > 0) invalidChar.Append(",");
                invalidChar.Append("'?'");
            }
            if (invalidChar.Length > 0)
                throw new Exception(String.Format("{0} name cannot contain {1}.", this.GetType().Name, invalidChar));
            if (String.IsNullOrWhiteSpace(name))
                throw new Exception(String.Format("{0} name cannot empty.", this.GetType().Name));
        }
    }
}
