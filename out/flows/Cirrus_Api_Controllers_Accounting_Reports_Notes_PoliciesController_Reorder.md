[web] PUT /api/accounting/reports/notes/policies/{id:Guid}/reorder/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.Reorder)  [L82–L105] status=200 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository (methods: WriteQuery,ReadQuery) [L90]
  └─ write Policy [L90]
  └─ query Policy [L85]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ Policy writes=1 reads=1

