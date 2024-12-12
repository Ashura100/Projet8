using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarVr;
using UnityEngine.UI;
using NUnit.Framework;
using TMPro;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class CarVRTest : MonoBehaviour
{
    private CatalogManager catalogManager;
    private CheckInternet checkInternet;
    private UIManager uiManager;

    [SetUp]
    public void Setup()
    {
        // Initialisation du CatalogManager
        GameObject catalogManagerObject = new GameObject();
        catalogManager = catalogManagerObject.AddComponent<CatalogManager>();
        CatalogManager.instance = catalogManager;

        // Initialisation du CheckInternet
        GameObject checkInternetObject = new GameObject();
        checkInternet = checkInternetObject.AddComponent<CheckInternet>();


    }

    [TearDown]
    public void TearDown()
    {
        // Nettoyage après chaque test
        if (catalogManager != null)
            Object.Destroy(catalogManager.gameObject);

        if (checkInternet != null)
            Object.Destroy(checkInternet.gameObject);
    }

    [UnityTest]
    public IEnumerator TestAPIRequest()
    {
        // Appeler la méthode GetCarData
        yield return catalogManager.StartCoroutine(catalogManager.GetCarData());

        // Vérifier que la liste a été remplie
        Assert.IsFalse(catalogManager.carList.Count == 0, "La liste des voitures doit être remplie après l'appel à l'API.");
    }

    [UnityTest]
    public IEnumerator TestVehicleSwitching()
    {
        SceneManager.LoadScene("New Scene", LoadSceneMode.Single);
        yield return new WaitForSeconds(2); // Attendre une frame pour assurer le chargement complet

        uiManager = GameObject.FindAnyObjectByType<UIManager>();

        Assert.AreEqual("AMC", uiManager.currentCar.name, "Le premier véhicule devrait être Car1");

        // Passer au véhicule suivant
        uiManager.NextVehicle();

        // Vérifier que le véhicule actuel a changé
        yield return null; // Attendre un frame pour que la méthode NextVehicle soit terminée
        Assert.AreEqual("Toyota", uiManager.currentCar.name, "Le véhicule devrait maintenant être Car2");

        // Revenir au véhicule précédent
        uiManager.PreviousVehicle();

        // Vérifier que le véhicule actuel est revenu à l'original
        yield return null;
        Assert.AreEqual("AMC", uiManager.currentCar.name, "Le véhicule devrait être revenu à Car1");
    }


    [UnityTest]
    public IEnumerator TestInternetConnection()
    {
        // Vérifier l'état initial de la connexion
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Assert.Fail("Pas de connexion Internet.");
        }

        // Vérifier la connexion Internet via l'appel à VerifyInternetConnection
        yield return checkInternet.StartCoroutine(checkInternet.VerifyInternetConnection());

        // Si aucune erreur, le test passe
        Assert.Pass("Connexion Internet réussie.");
    }
}
