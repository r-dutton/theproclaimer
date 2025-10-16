[web] DELETE /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Delete)  [L85–L90] status=200 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.Remove [L89]
  └─ calls PolicyLayoutRepository.WriteQuery [L88]
  └─ delete PolicyLayout [L89]
    └─ reads_from PolicyLayouts
  └─ write PolicyLayout [L88]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method WriteQuery [L88]
      └─ ... (no implementation details available)

