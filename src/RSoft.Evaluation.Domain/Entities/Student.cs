using RSoft.Framework.Cross.Entities;
using RSoft.Framework.Domain.Contracts;
using RSoft.Framework.Domain.Entities;
using RSoft.Framework.Domain.ValueObjects;
using System;

namespace RSoft.Evaluation.Domain.Entities
{

    /// <summary>
    /// Student user who can take the exams
    /// </summary>
    public class Student : EntityIdAuditBase<Guid, Student>, IEntity, IAuditAuthor<Guid>, IActive
    {

        #region Constructors

        /// <summary>
        /// Iniatize a new instance of Student 
        /// </summary>
        public Student() : base(Guid.NewGuid())
        {
            Initialize();
        }

        /// <summary>
        /// Iniatize a new instance of Student 
        /// </summary>
        /// <param name="id">Student id value</param>
        public Student(Guid id) : base(id)
        {
            Initialize();
        }

        /// <summary>
        /// Iniatize a new instance of Student 
        /// </summary>
        /// <param name="id">Student id text</param>
        public Student(string id) : base()
        {
            Id = new Guid(id);
            Initialize();
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public bool IsActive { get; set; }

        /// <summary>
        /// Student document number (withou mask)
        /// </summary>
        public string Document { get; set; }

        /// <summary>
        /// Student full name
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// Student date of birth
        /// </summary>
        public DateTime? BornDate { get; set; }

        /// <summary>
        /// Student e-mail
        /// </summary>
        public Email Email { get; set; }

        #endregion

        #region Navigation/Lazy

        #endregion

        #region Local methods

        /// <summary>
        /// Iniatialize objects/properties/fields with default values
        /// </summary>
        private void Initialize()
        {
            IsActive = true;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public override void Validate()
        {
            //BACKLOG: Globalization
            if (CreatedAuthor != null) AddNotifications(CreatedAuthor.Notifications);
            if (ChangedAuthor != null) AddNotifications(ChangedAuthor.Notifications);
            AddNotifications(Name.Notifications);
            AddNotifications(Email.Notifications);
            AddNotifications(new RequiredValidationContract<string>(Email?.Address, $"Email.{nameof(Email.Address)}", "Email is required").Contract.Notifications);
            AddNotifications(new PastDateValidationContract(BornDate, "Born date", "Burn date is required").Contract.Notifications);
        }

        #endregion

    }
}
