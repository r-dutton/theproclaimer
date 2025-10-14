[web] GET /workpapers/v1/custom-statuses/{statusId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.Get)  [L42–L48]
  └─ maps_to CustomStatusDto [L45]
    └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
    └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
  └─ calls CustomStatusRepository.ReadQuery [L45]
  └─ queries CustomStatus [L45]
    └─ reads_from CustomStatuses
  └─ uses_service IControlledRepository<CustomStatus> (AddScoped)
    └─ method ReadQuery [L45]

