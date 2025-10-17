[web] POST /api/accounting/reports/notes/disclosure-templates/inherit  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.CreateInherited)  [L153–L163] status=201 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository (methods: Add,ReadQuery) [L161]
  └─ insert DisclosureTemplate [L161]
    └─ reads_from DisclosureTemplates
  └─ query DisclosureTemplate [L156]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ DisclosureTemplate writes=1 reads=1

