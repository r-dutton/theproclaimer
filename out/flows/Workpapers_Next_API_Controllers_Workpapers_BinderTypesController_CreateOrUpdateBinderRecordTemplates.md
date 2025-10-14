[web] PUT /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.CreateOrUpdateBinderRecordTemplates)  [L109–L128]
  └─ calls BinderTypeRecordTemplateSetRepository.Add [L122]
  └─ calls BinderTypeRecordTemplateSetRepository.WriteQuery [L114]
  └─ writes_to BinderTypeRecordTemplateSet [L122]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ writes_to BinderTypeRecordTemplateSet [L114]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method WriteQuery [L114]

