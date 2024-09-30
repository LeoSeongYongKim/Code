using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace WebAPI.Controllers
{
    #region #. models
    public class DeviceModel
    {
        [JsonPropertyName("maker")]
        public string Maker { get; set; }
        [JsonPropertyName("machine_cd")]
        public string MachineCode { get; set; }
        [JsonPropertyName("apimachineinfo")]
        public string ApiMachineInfo { get; set; }
        [JsonPropertyName("useyn")]
        public bool UseYN { get; set; }
    }

    public class DeviceInfoModel
    {
        [JsonPropertyName("machine_cd")]
        public string MachineCode { get; set; }
        [JsonPropertyName("facility_nm")]
        public string FacilityName { get; set; }
        [JsonPropertyName("facility_accnt")]
        public string FacilityAccnt { get; set; }
        [JsonPropertyName("facility_lvl1")]
        public string FacilityLvl1 { get; set; }
        [JsonPropertyName("facility_lvl2")]
        public string FacilityLvl2 { get; set; }
        [JsonPropertyName("asst_cd1")]
        public string AsstCode1 { get; set; }
        [JsonPropertyName("asst_cd2")]
        public string AsstCode2 { get; set; }
        [JsonPropertyName("set_plant")]
        public string SetPlant { get; set; }
        [JsonPropertyName("set_place")]
        public string SetPlace { get; set; }
        [JsonPropertyName("set_dt")]
        public string SetDT { get; set; }
        [JsonPropertyName("set_co")]
        public string SetCo { get; set; }
        [JsonPropertyName("set_pur_cod")]
        public string SetPurCod { get; set; }
        [JsonPropertyName("set_pur_dt")]
        public object SetPurDT { get; set; }
        [JsonPropertyName("set_prod_co")]
        public string SetProdCo { get; set; }
        [JsonPropertyName("set_prod_amt")]
        public object SetProdAmt { get; set; }
        [JsonPropertyName("set_model_sts")]
        public string SetModelSts { get; set; }
        [JsonPropertyName("set_pm_dt")]
        public object SetPmDt { get; set; }
        [JsonPropertyName("set_pm_reason")]
        public string SetPmReason { get; set; }
        [JsonPropertyName("set_plant_sts")]
        public string SetPlantSts { get; set; }
        [JsonPropertyName("set_currency")]
        public string SetCurrency { get; set; }
        [JsonPropertyName("set_use_yn")]
        public string SetUseYN { get; set; }
        [JsonPropertyName("set_emp_no")]
        public string SetEmpNo { get; set; }
        [JsonPropertyName("set_updt_emp_no")]
        public string SetUpdtEmpNo { get; set; }
        [JsonPropertyName("set_updt_dt")]
        public string SetUpdtDt { get; set; }
    }


    public class ReqAddDevice
    {
        [JsonPropertyName("device")]
        public DeviceModel Device { get; set; }
        [JsonPropertyName("device_info")]
        public DeviceInfoModel DeviceInfo { get; set; }
    }

    public class ResAddDevice
    {
        [JsonPropertyName("result_cd")]
        public int ResultCode { get; set; }
        [JsonPropertyName("result_msg")]
        public string ResultMessage { get; set; }
        [JsonPropertyName("device")]
        public DeviceModel device { get; set; }
    }

    public class ReqUpdateDevice
    {
        [JsonPropertyName("device")]
        public DeviceModel Device { get; set; }
        [JsonPropertyName("device_info")]
        public DeviceInfoModel DeviceInfo { get; set; }
    }

    public class ResUpdateDevice
    {
        [JsonPropertyName("result_cd")]
        public int ResultCode { get; set; }
        [JsonPropertyName("result_msg")]
        public string ResultMessage { get; set; }
        [JsonPropertyName("device")]
        public DeviceModel device { get; set; }

    }

    public class ResDeleteDevice
    {
        [JsonPropertyName("result_cd")]
        public int ResultCode { get; set; }
        [JsonPropertyName("result_msg")]
        public string ResultMessage { get; set; }
    }
    #endregion
    public class WebAPI : ControllerBase
    {
        #region #. temp data
        private static List<DeviceModel> Devices = new ()
        {
            new DeviceModel{
            Maker = "interx",
            MachineCode = "MK1005",
            ApiMachineInfo = "interx_1005",
            UseYN = true
            },
            new DeviceModel{
            Maker = "interx",
            MachineCode = "MK1006",
            ApiMachineInfo = "interx_1006",
            UseYN = false
            }
        };
        public static List<DeviceInfoModel> DeviceInfos = new()
        {
            new DeviceInfoModel
            {
                MachineCode = "MK1005",
                FacilityName= "호퍼11호기",
                FacilityAccnt = "7",
                FacilityLvl1 = "",
                FacilityLvl2 = "",
                AsstCode1 = "",
                AsstCode2 = "",
                SetPlant = "U1",
                SetPlace = "180TON",
                SetCo = "",
                SetPurCod =  "",
                SetPurDT = null,
                SetProdCo = "",
                SetProdAmt=  null,
                SetModelSts =  "",
                SetPmDt =  null,
                SetPmReason = "",
                SetPlantSts=  "",
                SetCurrency = "",
                SetUseYN = "Y",
                SetEmpNo = "CFESOF0999",
                SetDT = "2022-01-20 23:36:18.000Z",
                SetUpdtEmpNo = "0075",
                SetUpdtDt = "2022-01-20 23:36:18.000Z"
            },
            new DeviceInfoModel {
                MachineCode = "MK1006",
                FacilityName= "호퍼12호기",
                FacilityAccnt = "7",
                FacilityLvl1 = "",
                FacilityLvl2 = "",
                AsstCode1 = "",
                AsstCode2 = "",
                SetPlant = "U2",
                SetPlace = "150TON",
                SetCo = "",
                SetPurCod =  "",
                SetPurDT = null,
                SetProdCo = "",
                SetProdAmt=  null,
                SetModelSts =  "",
                SetPmDt =  null,
                SetPmReason = "",
                SetPlantSts=  "",
                SetCurrency = "",
                SetUseYN = "Y",
                SetEmpNo = "CFESOF0999",
                SetDT = "2022-11-20 22:31:18.000Z",
                SetUpdtEmpNo = "0075",
                SetUpdtDt = "2023-01-20 12:36:18.000Z"
            }
        };
        #endregion
        #region #. apis
        /// <summary>
        /// 장비 목록 조회 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/devices")]
        [ProducesResponseType(typeof(DeviceModel), 200)]
        public IEnumerable<DeviceModel> GetDevices()
        {
            List<DeviceModel> devices = new();
            devices = Devices;
            return devices;
        }
        /// <summary>
        /// 장비 내 파라미터 조회
        /// </summary>
        /// <param name="machine_cod">
        /// id = machine_cod
        /// </param>
        /// <returns></returns>
        [HttpGet("/devices/{id}")]
        [ProducesResponseType(typeof(DeviceInfoModel), 200)]
        public DeviceInfoModel GetDeviceInfo(string id)
        {
            DeviceInfoModel result = new DeviceInfoModel();
            result = DeviceInfos.Where(x => x.MachineCode == id).FirstOrDefault();

            return result;
        }
        /// <summary>
        /// 장비 / 파라미터 추가
        /// </summary>
        /// <param name="req_device_info">
        /// ReqAddDevice
        /// </param>
        /// <returns>ResAddDevice</returns>        
        [HttpPost("/devices")]
        [ProducesResponseType(typeof(ResAddDevice), 200)]
        public ResAddDevice AddDevice([FromBody] ReqAddDevice req)
        {
            ResAddDevice result = new ResAddDevice();
            DeviceModel insertDevice = req.Device;
            DeviceInfoModel insertDeviceInfo = req.DeviceInfo;
            try { 
                Devices.Add(insertDevice);
                DeviceInfos.Add(insertDeviceInfo);
            }catch(Exception ex){
                if (Devices.Contains(insertDevice))
                {
                    Devices.Remove(insertDevice);
                }
                result.ResultCode = -500;
                result.ResultMessage = ex.Message;
            };
            
           if(Devices.Contains(insertDevice) && DeviceInfos.Contains(insertDeviceInfo))
            {
                result.ResultCode = 200;
                result.ResultMessage = "Success";
            }

            return result;
        }
        /// <summary>
        /// 장비 / 파라미터 정보 수정 
        /// </summary>
        /// <returns></returns>
        [HttpPut("/books/{id}")]
        [ProducesResponseType(typeof(ResUpdateDevice), 200)]
        public ResUpdateDevice UpdateDevice([FromRoute] string id,[FromBody] ReqUpdateDevice req)
        {
            ResUpdateDevice result = new();
            PropertyInfo[] devicePros = typeof(DeviceModel).GetProperties();
            PropertyInfo[] deviceInfoPros = typeof(DeviceInfoModel).GetProperties();
            DeviceModel targetDevce = Devices.Where(x=>x.MachineCode == id).FirstOrDefault();
            DeviceInfoModel targetDeviceInfo = DeviceInfos.Where(x => x.MachineCode == id).FirstOrDefault();
            if (targetDevce == null|| targetDeviceInfo== null)
            {
                result.ResultCode = 404;
                result.ResultMessage = "Device not found";
                return result;
            }
            try
            {
                foreach (var pro1 in devicePros)
                {
                    if ("" != pro1.GetValue(req.Device))
                    {
                        var value = pro1.GetValue(req.Device);
                        pro1.SetValue(targetDevce, value);
                    }
                }

                foreach (var pro2 in deviceInfoPros)
                {
                    if ("" != pro2.GetValue(req.DeviceInfo))
                    {
                        var value = pro2.GetValue(req.DeviceInfo);
                        pro2.SetValue(targetDeviceInfo, value);
                    }
                }

                result.ResultCode = 200;
                result.ResultMessage = "Success";
            }
            catch (Exception ex)
            {
                result.ResultCode = -500;
                result.ResultMessage = ex.Message;
            }
            return result;
        }

        [HttpDelete("/books/{id}")]
        [ProducesResponseType(typeof(ResDeleteDevice), 200)]
        public ResDeleteDevice DeleteDevice(string id)
        {
            ResDeleteDevice result = new ResDeleteDevice();
            DeviceModel targetDevice = Devices.Where(x=>x.MachineCode ==id).FirstOrDefault();
            if(targetDevice == null)
            {
                result.ResultCode = 404;
                result.ResultMessage = "Device not found";
                return result;
            }
            try
            {
                targetDevice.UseYN = false;
            }
            catch
            {
                result.ResultCode = -500;
                result.ResultMessage = "fail";
            }
            result.ResultCode = 200;
            result.ResultMessage = "Success";
            return result;  
        }
        #endregion
    }    
}
