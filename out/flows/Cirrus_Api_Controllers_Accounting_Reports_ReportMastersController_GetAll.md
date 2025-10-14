[web] GET /api/accounting/reports/masters/forfile/{fileId:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.ReportMastersController.GetAll)  [L97–L119] [auth=user]
  └─ maps_to ReportMasterLightDto [L113]
    └─ automapper.registration CirrusMappingProfile (ReportMaster->ReportMasterLightDto) [L571]
  └─ calls FileRepository.ReadQuery [L100]
  └─ calls ReportMasterRepository.ReadQuery [L113]
  └─ calls EntityRepository.ReadQuery [L107]
  └─ queries File [L100]
    └─ reads_from Files
  └─ queries ReportMaster [L113]
    └─ reads_from ReportMasters
  └─ queries Entity [L107]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L107]
  └─ uses_service IControlledRepository<File>
    └─ method ReadQuery [L100]
  └─ uses_service IControlledRepository<ReportMaster>
    └─ method ReadQuery [L113]

