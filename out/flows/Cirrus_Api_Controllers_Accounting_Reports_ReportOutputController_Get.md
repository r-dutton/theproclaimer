[web] GET /api/accounting/reports/output/included-accounts  (Cirrus.Api.Controllers.Accounting.Reports.ReportOutputController.Get)  [L51–L128] status=200 [auth=user]
  └─ maps_to AccountUltraLightDto [L117]
    └─ automapper.registration CirrusMappingProfile (Account->AccountUltraLightDto) [L312]
  └─ calls FileRepository.ReadQuery [L82]
  └─ calls AccountRepository.ReadQuery [L66]
  └─ query File [L82]
    └─ reads_from Files
  └─ query Account [L66]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Account writes=0 reads=1
      └─ File writes=0 reads=1
    └─ mappings 1
      └─ AccountUltraLightDto

