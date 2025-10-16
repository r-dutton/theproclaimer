[web] DELETE /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Delete)  [L192–L199] status=200 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository.Remove [L197]
  └─ calls PolicyRepository.WriteQuery [L195]
  └─ delete Policy [L197]
  └─ write Policy [L195]
  └─ uses_service IControlledRepository<Policy>
    └─ method WriteQuery [L195]
      └─ ... (no implementation details available)

