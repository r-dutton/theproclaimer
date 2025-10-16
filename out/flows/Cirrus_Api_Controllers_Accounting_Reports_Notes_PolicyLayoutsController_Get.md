[web] GET /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Get)  [L51–L56] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to PolicyLayoutDto [L54]
    └─ automapper.registration CirrusMappingProfile (PolicyLayout->PolicyLayoutDto) [L783]
  └─ calls PolicyLayoutRepository.ReadQuery [L54]
  └─ query PolicyLayout [L54]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)

