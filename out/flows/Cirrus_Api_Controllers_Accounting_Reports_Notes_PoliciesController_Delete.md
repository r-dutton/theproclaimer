[web] DELETE /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Delete)  [L192–L199] [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Remove [L197]
  └─ calls PolicyRepository.WriteQuery [L195]
  └─ writes_to Policy [L197]
  └─ writes_to Policy [L195]
  └─ uses_service IControlledRepository<Policy>
    └─ method WriteQuery [L195]

