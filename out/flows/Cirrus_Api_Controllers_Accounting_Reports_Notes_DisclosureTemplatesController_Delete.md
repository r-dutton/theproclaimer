[web] DELETE /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Delete)  [L180–L187] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository (methods: Remove,WriteQuery) [L185]
  └─ delete DisclosureTemplate [L185]
    └─ reads_from DisclosureTemplates
  └─ write DisclosureTemplate [L183]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DisclosureTemplate writes=2 reads=0

