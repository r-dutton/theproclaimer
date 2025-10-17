[web] POST /api/accounting/reports/notes/policies/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PoliciesController.CreateInherited)  [L164–L175] status=201 [auth=Authentication.UserPolicy]
  └─ calls PolicyRepository (methods: Add,ReadQuery) [L173]
  └─ insert Policy [L173]
  └─ query Policy [L167]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ Policy writes=1 reads=1

