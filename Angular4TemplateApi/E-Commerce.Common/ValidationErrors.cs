using ECommerce.Entities;
using FluentValidation.Results;
using System.Collections.Generic;

namespace ECommerce.Common
{
    /// <summary>
    /// Validation Errors
    /// </summary>
    public static class ValidationErrors
    {
        /// <summary>
        /// Populate Validation Errors
        /// </summary>
        /// <param name="failures"></param>
        /// <returns></returns>
        public static TransactionalBase PopulateValidationErrors(IList<ValidationFailure> failures)
        {
            TransactionalBase transaction = new TransactionalBase();

            transaction.ReturnStatus = false;
            foreach (ValidationFailure error in failures)
            {
                if (transaction.ValidationErrors.ContainsKey(error.PropertyName) == false)
                    transaction.ValidationErrors.Add(error.PropertyName, error.ErrorMessage);

                transaction.ReturnMessage.Add(error.ErrorMessage);
            }

            return transaction;
        }
    }
}