[web] POST /api/accounting/reports/notes/policies/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Create)  [L133–L158] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Add [L156]
  └─ calls PolicyRepository.WriteQuery [L151]
  └─ calls PolicyRepository.ReadQuery [L139]
  └─ insert Policy [L156]
  └─ query Policy [L139]
  └─ write Policy [L151]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L139]
      └─ ... (no implementation details available)

