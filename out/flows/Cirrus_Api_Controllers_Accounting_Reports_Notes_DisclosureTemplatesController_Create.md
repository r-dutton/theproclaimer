[web] POST /api/accounting/reports/notes/disclosure-templates/master/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Create)  [L121–L147] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository (methods: Add,WriteQuery,ReadQuery) [L145]
  └─ insert DisclosureTemplate [L145]
    └─ reads_from DisclosureTemplates
  └─ write DisclosureTemplate [L137]
    └─ reads_from DisclosureTemplates
  └─ query DisclosureTemplate [L127]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=1)
      └─ DisclosureTemplate writes=2 reads=1

