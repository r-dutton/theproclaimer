[web] GET /workpapers/v1/custom-statuses/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.GetStatusesQuery)  [L55–L61] status=200
  └─ maps_to CustomStatusDto [L58]
    └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
    └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
  └─ calls CustomStatusRepository.ReadQuery [L58]
  └─ query CustomStatus [L58]
    └─ reads_from CustomStatuses
  └─ uses_service IControlledRepository<CustomStatus> (AddScoped)
    └─ method ReadQuery [L58]
      └─ ... (no implementation details available)

