[web] PUT /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.CreateOrUpdateBinderRecordTemplates)  [L109–L128] status=200
  └─ calls BinderTypeRecordTemplateSetRepository.Add [L122]
  └─ calls BinderTypeRecordTemplateSetRepository.WriteQuery [L114]
  └─ insert BinderTypeRecordTemplateSet [L122]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ write BinderTypeRecordTemplateSet [L114]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ uses_service IControlledRepository<BinderTypeRecordTemplateSet>
    └─ method WriteQuery [L114]
      └─ ... (no implementation details available)

