[web] DELETE /api/accounting/reports/notes/policy-layouts/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyLayoutsController.Delete)  [L85–L90] status=200 [auth=Authentication.AdminPolicy]
  └─ calls PolicyLayoutRepository (methods: Remove,WriteQuery) [L89]
  └─ delete PolicyLayout [L89]
    └─ reads_from PolicyLayouts
  └─ write PolicyLayout [L88]
    └─ reads_from PolicyLayouts
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ PolicyLayout writes=2 reads=0

