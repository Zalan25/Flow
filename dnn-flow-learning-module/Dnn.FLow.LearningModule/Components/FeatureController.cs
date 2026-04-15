/*
' Copyright (c) 2026 Flow
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Collections.Generic;

namespace Dnn.Flow.Dnn.FLow.LearningModule.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Dnn.FLow.LearningModule
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<Dnn.FLow.LearningModuleInfo> colDnn.FLow.LearningModules = GetDnn.FLow.LearningModules(ModuleID);
        //if (colDnn.FLow.LearningModules.Count != 0)
        //{
        //    strXML += "<Dnn.FLow.LearningModules>";

        //    foreach (Dnn.FLow.LearningModuleInfo objDnn.FLow.LearningModule in colDnn.FLow.LearningModules)
        //    {
        //        strXML += "<Dnn.FLow.LearningModule>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objDnn.FLow.LearningModule.Content) + "</content>";
        //        strXML += "</Dnn.FLow.LearningModule>";
        //    }
        //    strXML += "</Dnn.FLow.LearningModules>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlDnn.FLow.LearningModules = DotNetNuke.Common.Globals.GetContent(Content, "Dnn.FLow.LearningModules");
        //foreach (XmlNode xmlDnn.FLow.LearningModule in xmlDnn.FLow.LearningModules.SelectNodes("Dnn.FLow.LearningModule"))
        //{
        //    Dnn.FLow.LearningModuleInfo objDnn.FLow.LearningModule = new Dnn.FLow.LearningModuleInfo();
        //    objDnn.FLow.LearningModule.ModuleId = ModuleID;
        //    objDnn.FLow.LearningModule.Content = xmlDnn.FLow.LearningModule.SelectSingleNode("content").InnerText;
        //    objDnn.FLow.LearningModule.CreatedByUser = UserID;
        //    AddDnn.FLow.LearningModule(objDnn.FLow.LearningModule);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<Dnn.FLow.LearningModuleInfo> colDnn.FLow.LearningModules = GetDnn.FLow.LearningModules(ModInfo.ModuleID);

        //foreach (Dnn.FLow.LearningModuleInfo objDnn.FLow.LearningModule in colDnn.FLow.LearningModules)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objDnn.FLow.LearningModule.Content, objDnn.FLow.LearningModule.CreatedByUser, objDnn.FLow.LearningModule.CreatedDate, ModInfo.ModuleID, objDnn.FLow.LearningModule.ItemId.ToString(), objDnn.FLow.LearningModule.Content, "ItemId=" + objDnn.FLow.LearningModule.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
