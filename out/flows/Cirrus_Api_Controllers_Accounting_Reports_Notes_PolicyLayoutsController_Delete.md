[web] DELETE /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Delete)  [L85–L90] [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository.Remove [L89]
  └─ calls PolicyLayoutRepository.WriteQuery [L88]
  └─ writes_to PolicyLayout [L89]
    └─ reads_from PolicyLayouts
  └─ writes_to PolicyLayout [L88]
    └─ reads_from PolicyLayouts
  └─ uses_service IControlledRepository<PolicyLayout>
    └─ method WriteQuery [L88]

