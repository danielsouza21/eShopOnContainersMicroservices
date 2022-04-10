using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommandRequest>
    {
        private const int TOTAL_PRICE_ZERO_VALUE = 0;
        private const int USER_NAME_MAXIMUM_LENGTH = 50;

        //Class to validate all props of the Request
        //"Pre Proccessor Behavior" stage of MediatR workflow
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage($"{nameof(UpdateOrderCommandRequest.UserName)} is required.")
                .NotNull()
                .MaximumLength(USER_NAME_MAXIMUM_LENGTH).WithMessage($"{nameof(UpdateOrderCommandRequest.UserName)} must not exceed {USER_NAME_MAXIMUM_LENGTH} characters.");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage($"{nameof(UpdateOrderCommandRequest.EmailAddress)} is required.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage($"{nameof(UpdateOrderCommandRequest.TotalPrice)} is required.")
                .GreaterThan(TOTAL_PRICE_ZERO_VALUE).WithMessage($"{nameof(UpdateOrderCommandRequest.TotalPrice)} should be greater than {TOTAL_PRICE_ZERO_VALUE}.");
        }
    }
}
