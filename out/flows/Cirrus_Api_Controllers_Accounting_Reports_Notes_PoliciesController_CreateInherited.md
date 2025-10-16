[web] POST /api/accounting/reports/notes/policies/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.CreateInherited)  [L164–L175] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Add [L173]
  └─ calls PolicyRepository.ReadQuery [L167]
  └─ insert Policy [L173]
  └─ query Policy [L167]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L167]
      └─ ... (no implementation details available)

