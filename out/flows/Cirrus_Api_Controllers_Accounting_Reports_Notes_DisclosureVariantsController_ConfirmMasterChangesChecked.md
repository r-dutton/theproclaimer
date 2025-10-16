[web] PUT /api/accounting/reports/notes/disclosures/variants/{id:Guid}/confirmMasterChangesChecked  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.ConfirmMasterChangesChecked)  [L153–L162] status=200 [auth=Authentication.UserPolicy]
  └─ calls DisclosureVariantRepository.WriteQuery [L156]
  └─ write DisclosureVariant [L156]
    └─ reads_from DisclosureVariants
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DisclosureVariant writes=1 reads=0

