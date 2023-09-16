using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MerchantAPI.Models.Errors;
using MerchantAPI.V1.Models.RequestModels;

namespace MerchantAPI.V1.Models.Validations;

public class MerchantUpdateRequestModelValidator: AbstractValidator<MerchantUpdateRequestModel>
{
    public MerchantUpdateRequestModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithState(x =>
            throw new EmptyMerchantName(x.Name));
        RuleFor(x => x.CityCode).NotEmpty().WithState(x=> throw new EmptyCityCode())
            .GreaterThanOrEqualTo(1).WithState(x => throw new NonpositiveCityCode(x.CityCode));
        RuleFor(x => x.City).NotEmpty().WithState(x =>
            throw new EmptyCityName(x.City));
        RuleFor(x => x.Neighborhood).NotEmpty().WithState(x =>
            throw new EmptyNeighborhood(x.Neighborhood));
    }
}