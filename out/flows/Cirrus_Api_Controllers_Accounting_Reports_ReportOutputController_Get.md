[web] GET /api/accounting/reports/output/included-accounts  (Cirrus.Api.Controllers.Accounting.Reports.ReportOutputController.Get)  [L51–L128] status=200 [auth=user]
  └─ maps_to AccountUltraLightDto [L117]
    └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
  └─ maps_to AccountUltraLightDto [L102]
    └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
  └─ maps_to AccountUltraLightDto [L96]
  └─ maps_to AccountUltraLightDto [L66]
    └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
  └─ calls FileRepository.ReadQuery [L82]
  └─ calls AccountRepository.ReadQuery [L66]
  └─ query File [L82]
    └─ reads_from Files
  └─ query Account [L66]
  └─ uses_service IControlledRepository<Account>
    └─ method ReadQuery [L66]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L82]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L96]
      └─ ... (no implementation details available)

