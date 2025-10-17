[web] DELETE /api/accounting/reports/notes/policies/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Delete)  [L192–L199] status=200 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository (methods: Remove,WriteQuery) [L197]
  └─ delete Policy [L197]
  └─ write Policy [L195]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Policy writes=2 reads=0

