using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SVLCmanage
{
    public class DataValidationClass : ViewModelBaseClass, IDataErrorInfo
    {
      
        public DataValidationClass()
        {

        }

        #region DataValidation
      
        private string namestd;
        public string NameStudent
        {
            get { return namestd; }
            set { namestd = value; RaisePropertyChanged("NameStudent"); }
        }

        private string nameengstd;
        public string NameEngStudent
        {
            get { return nameengstd; }
            set { nameengstd = value; RaisePropertyChanged("NameEngStudent"); }
        }
       
        private string tel2;
        public string Tel2
        {
            get { return tel2; }
            set { tel2 = value; RaisePropertyChanged("Tel2"); }
        }
       
        private string year1;
        public string Year1
        {
            get { return year1; }
            set { year1 = value; RaisePropertyChanged("Year1"); }
        }

        private string year2;
        public string Year2
        {
            get { return year2; }
            set { year2 = value; RaisePropertyChanged("Year2"); }
        }

        private string subjectN;
        public string SubjectN
        {
            get { return subjectN; }
            set { subjectN = value; RaisePropertyChanged("SubjectN"); }
        }

        private string subjectEN;
        public string SubjectEN
        {
            get { return subjectEN; }
            set { subjectEN = value; RaisePropertyChanged("SubjectEN"); }
        }

        private string subjectId;
        public string SubjectId
        {
            get { return subjectId; }
            set { subjectId = value; RaisePropertyChanged("SubjectId"); }
        }
           
        private string subjectTeach;
        public string SubjectTeach
        {
            get { return subjectTeach; }
            set { subjectTeach = value; RaisePropertyChanged("SubjectTeach"); }
        }

        private string subjectTYear;
        public string SubjectTYear
        {
            get { return subjectTYear; }
            set { subjectTYear = value; RaisePropertyChanged("SubjectTYear"); }
        }

        private string subjectCredit;
        public string SubjectCredit
        {
            get { return subjectCredit; }
            set { subjectCredit = value; RaisePropertyChanged("SubjectCredit"); }
        }

        private string subjectHour;
        public string SubjectHour
        {
            get { return subjectHour; }
            set { subjectHour = value; RaisePropertyChanged("SubjectHour"); }
        }

        private string byear;
        public string Byear
        {
            get { return byear; }
            set { byear = value; RaisePropertyChanged("Byear"); }
        }
        #endregion

        public bool IsValidSave
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NameStudent) && !string.IsNullOrWhiteSpace(NameEngStudent) && !string.IsNullOrWhiteSpace(Byear)
                && !string.IsNullOrWhiteSpace(Tel2) && !string.IsNullOrWhiteSpace(Year1) && !string.IsNullOrWhiteSpace(Year2);
            }
        }

        public bool IsValidSave2
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SubjectTeach)
                && !string.IsNullOrWhiteSpace(SubjectTYear);
            }
        }

        public bool IsValidSave3
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SubjectId) && !string.IsNullOrWhiteSpace(SubjectN) && !string.IsNullOrWhiteSpace(SubjectHour)
                && !string.IsNullOrWhiteSpace(SubjectEN) && !string.IsNullOrWhiteSpace(SubjectCredit);
            }
        }


        private string error = string.Empty;
        public string Error
        {
            get { return error; }
        }

        public string this[string columnName]
        {
            get
            {

                error = string.Empty;              
                if (columnName == "NameStudent" && string.IsNullOrWhiteSpace(NameStudent))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "NameEngStudent" && string.IsNullOrWhiteSpace(NameEngStudent))
                {
                    error = "ວ່າງເປົ່າ";
                }              
                else if (columnName == "Tel2" && string.IsNullOrWhiteSpace(Tel2))
                {
                    error = "ວ່າງເປົ່າ";
                }              
                else if (columnName == "Year1" && string.IsNullOrWhiteSpace(Year1))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "Year2" && string.IsNullOrWhiteSpace(Year2))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "Byear" && string.IsNullOrWhiteSpace(Byear))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "SubjectId" && string.IsNullOrWhiteSpace(SubjectId))
                {
                    error = "ວ່າງເປົ່າ";
                }               
                else if (columnName == "SubjectN" && string.IsNullOrWhiteSpace(SubjectN))
                {
                    error = "ວ່າງເປົ່າ";
                }               
                else if (columnName == "SubjectTeach" && string.IsNullOrWhiteSpace(SubjectTeach))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "SubjectEN" && string.IsNullOrWhiteSpace(SubjectEN))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "SubjectCredit" && string.IsNullOrWhiteSpace(SubjectCredit))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "SubjectHour" && string.IsNullOrWhiteSpace(SubjectHour))
                {
                    error = "ວ່າງເປົ່າ";
                }
                else if (columnName == "SubjectTYear" && string.IsNullOrWhiteSpace(SubjectTYear))
                {
                    error = "ວ່າງເປົ່າ";
                }

                RaisePropertyChanged("IsValidSave");
                RaisePropertyChanged("IsValidSave2");
                RaisePropertyChanged("IsValidSave3");
                return error;

            }
        }


    }
}
