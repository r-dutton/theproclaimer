[web] POST /api/accounting/reports/notes/policies/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.CreateInherited)  [L164–L175] [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Add [L173]
  └─ calls PolicyRepository.ReadQuery [L167]
  └─ queries Policy [L167]
  └─ writes_to Policy [L173]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L167]

