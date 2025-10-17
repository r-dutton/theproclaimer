[web] POST /api/binder-status-requirements/  (Workpapers.Next.API.Controllers.Workpapers.BinderStatusRequirementsController.Create)  [L43–L50] status=201
  └─ calls BinderStatusRequirementsRepository.Add [L47]
  └─ insert BinderStatusRequirements [L47]
    └─ reads_from BinderStatusRequirements
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ BinderStatusRequirements writes=1 reads=0

