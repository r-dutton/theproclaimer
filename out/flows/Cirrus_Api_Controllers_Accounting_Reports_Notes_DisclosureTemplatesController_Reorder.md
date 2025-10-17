[web] PUT /api/accounting/reports/notes/disclosure-templates/{id:Guid}/reorder/file  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Reorder)  [L76–L98] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository (methods: WriteQuery,ReadQuery) [L83]
  └─ write DisclosureTemplate [L83]
    └─ reads_from DisclosureTemplates
  └─ query DisclosureTemplate [L79]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ DisclosureTemplate writes=1 reads=1

