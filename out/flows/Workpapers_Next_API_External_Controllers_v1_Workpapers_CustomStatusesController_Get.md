[web] GET /workpapers/v1/custom-statuses/{statusId:Guid}  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.Get)  [L42–L48] status=200
  └─ maps_to CustomStatusDto [L45]
    └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
    └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
  └─ calls CustomStatusRepository.ReadQuery [L45]
  └─ query CustomStatus [L45]
    └─ reads_from CustomStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ CustomStatus writes=0 reads=1
    └─ mappings 1
      └─ CustomStatusDto

