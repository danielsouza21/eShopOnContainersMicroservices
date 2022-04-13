using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommandRequest> //IValidator
    {
        private const int TOTAL_PRICE_ZERO_VALUE = 0;
        private const int USER_NAME_MAXIMUM_LENGTH = 50;

        //Class to validate all props of the Request
        //"Pre Proccessor Behavior" stage of MediatR workflow (IPipelineBehaviour)
        public CheckoutOrderCommandValidator()
        {
            //AbstractValidator.RuleFor()
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage($"{nameof(CheckoutOrderCommandRequest.UserName)} is required.")
                .NotNull()
                .MaximumLength(USER_NAME_MAXIMUM_LENGTH).WithMessage($"{nameof(CheckoutOrderCommandRequest.UserName)} must not exceed {USER_NAME_MAXIMUM_LENGTH} characters.");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage($"{nameof(CheckoutOrderCommandRequest.EmailAddress)} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage($"{nameof(CheckoutOrderCommandRequest.TotalPrice)} is required.")
                .GreaterThan(TOTAL_PRICE_ZERO_VALUE).WithMessage($"{nameof(CheckoutOrderCommandRequest.TotalPrice)} should be greater than {TOTAL_PRICE_ZERO_VALUE}.");
        }
    }
}
