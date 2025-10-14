[web] POST /api/accounting/reports/notes/policies/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Create)  [L133–L158] [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Add [L156]
  └─ calls PolicyRepository.WriteQuery [L151]
  └─ calls PolicyRepository.ReadQuery [L139]
  └─ queries Policy [L139]
  └─ writes_to Policy [L156]
  └─ writes_to Policy [L151]
  └─ uses_service IControlledRepository<Policy>
    └─ method ReadQuery [L139]

