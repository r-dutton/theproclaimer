[web] GET /workpapers/v1/custom-statuses/  (Workpapers.Next.API.External.Controllers.v1.Workpapers.CustomStatusesController.GetStatusesQuery)  [L55–L61] status=200
  └─ maps_to CustomStatusDto [L58]
    └─ automapper.registration WorkpapersMappingProfile (CustomStatus->CustomStatusDto) [L399]
    └─ automapper.registration ExternalApiMappingProfile (CustomStatus->CustomStatusDto) [L146]
  └─ calls CustomStatusRepository.ReadQuery [L58]
  └─ query CustomStatus [L58]
    └─ reads_from CustomStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ CustomStatus writes=0 reads=1
    └─ mappings 1
      └─ CustomStatusDto

