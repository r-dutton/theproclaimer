[web] POST /api/accounting/reports/notes/policies/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Create)  [L133–L158] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository (methods: Add,WriteQuery,ReadQuery) [L156]
  └─ insert Policy [L156]
  └─ write Policy [L151]
  └─ query Policy [L139]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=1)
      └─ Policy writes=2 reads=1

