[web] PUT /api/binder-types/{id:guid}/record-template-set  (Workpapers.Next.API.Controllers.Workpapers.BinderTypesController.CreateOrUpdateBinderRecordTemplates)  [L109–L128] status=200
  └─ calls BinderTypeRecordTemplateSetRepository (methods: Add,WriteQuery) [L122]
  └─ insert BinderTypeRecordTemplateSet [L122]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ write BinderTypeRecordTemplateSet [L114]
    └─ reads_from BinderTypeRecordTemplateSets
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ BinderTypeRecordTemplateSet writes=2 reads=0

