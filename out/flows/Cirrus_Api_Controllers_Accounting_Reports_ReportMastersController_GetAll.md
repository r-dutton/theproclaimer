[web] GET /api/accounting/reports/masters/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAll)  [L97–L119] status=200 [auth=user]
  └─ maps_to ReportMasterLightDto [L113]
    └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterLightDto) [L571]
  └─ calls ReportMasterRepository.ReadQuery [L113]
  └─ calls EntityRepository.ReadQuery [L107]
  └─ calls FileRepository.ReadQuery [L100]
  └─ query ReportMaster [L113]
    └─ reads_from ReportMasters
  └─ query Entity [L107]
  └─ query File [L100]
    └─ reads_from Files
  └─ impact_summary
    └─ entities 3 (writes=0, reads=3)
      └─ Entity writes=0 reads=1
      └─ File writes=0 reads=1
      └─ ReportMaster writes=0 reads=1
    └─ mappings 1
      └─ ReportMasterLightDto

