using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Serializable]
    public class DemandInfo
    {
        /// <summary>
        /// 紧急程度名称
        /// </summary>
        private string emergencyName;
        public string EmergencyName
        {
            get { return emergencyName; }
            set { emergencyName = value; }
        }

        /// <summary>
        /// 发布人
        /// </summary>
        private string upline;
        public string Upline
        {
            get { return upline; }
            set { upline = value; }
        }
        

        private string confirmationName;
        public string ConfirmationName
        {
            get { return confirmationName; }
            set { confirmationName = value; }
        }
        

        private string modifierName;
        public string ModifierName
        {
            get { return modifierName; }
            set { modifierName = value; }
        }

        

        /// <summary>
        /// 电话
        /// </summary>
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private int demandId;
        public int DemandId
        {
            get { return demandId; }
            set { demandId = value; }
        }
        /// <summary>
        /// 产品编号
        /// </summary>
        private int? productId;
        public int? ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        /// <summary>
        /// 提交部门
        /// </summary>
        private string submissionDepartment;
        public string SubmissionDepartment
        {
            get { return submissionDepartment; }
            set { submissionDepartment = value; }
        }

        private string requirementDescription;
        public string RequirementDescription
        {
            get { return requirementDescription; }
            set { requirementDescription = value; }
        }

        private int emergencyId;
        public int EmergencyId
        {
            get { return emergencyId; }
            set { emergencyId = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private int founder;
        public int Founder
        {
            get { return founder; }
            set { founder = value; }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private DateTime? creationTime;
        public DateTime? CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }

        private int modifier;
        public int Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        private DateTime? modificationTime;
        public DateTime? ModificationTime
        {
            get { return modificationTime; }
            set { modificationTime = value; }
        }

        private int confirmationPerson;
        public int ConfirmationPerson
        {
            get { return confirmationPerson; }
            set { confirmationPerson = value; }
        }

        private DateTime? confirmTime;
        public DateTime? ConfirmTime
        {
            get { return confirmTime; }
            set { confirmTime = value; }
        }
     
        private DateTime? expectedOnlineTime;
        public DateTime? ExpectedOnlineTime
        {
            get { return expectedOnlineTime; }
            set { expectedOnlineTime = value; }
        }

        private string planContent;
        public string PlanContent
        {
            get { return planContent; }
            set { planContent = value; }
        }

        private int publisher;
        public int Publisher
        {
            get { return publisher; }
            set { publisher = value; }
        }

        private DateTime? scheduleTime;
        public DateTime? ScheduleTime
        {
            get { return scheduleTime; }
            set { scheduleTime = value; }
        }

        private DateTime? upTime;
        public DateTime? UpTime
        {
            get { return upTime; }
            set { upTime = value; }
        }
    }
}
