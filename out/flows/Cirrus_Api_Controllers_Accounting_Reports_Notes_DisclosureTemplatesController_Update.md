[web] PUT /api/accounting/reports/notes/disclosure-templates/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureTemplatesController.Update)  [L168–L175] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureTemplateRepository.WriteQuery [L171]
  └─ write DisclosureTemplate [L171]
    └─ reads_from DisclosureTemplates
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DisclosureTemplate writes=1 reads=0

