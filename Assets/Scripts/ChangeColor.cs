﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

	private List<Material> _coresEmContato = new List<Material>();
	private List<Material> _defaultMaterial = new List<Material>();
    private List<Colored.Corenum> _coresDisponiveis = new List<Colored.Corenum>();
    private List<Colored.Corenum> _corParede = new List<Colored.Corenum>();

    public GameObject model;

	// Use this for initialization
	void Start () {
		gameObject.layer = 9;
        /*
         * layer 9 e a do player quando nao esta camuflado
         * layer 11 e a layer dos enemies
         * layer 12 e a do player quando ele esta camuflado.
         * essas duas layers nao colidem uma com a outra
         * 
         **/
         // ALTERAR ISSO
        foreach (Transform child in model.GetComponentInChildren<Transform>())
        {
            _defaultMaterial.Add(child.GetComponent<Renderer>().material);
        }

        //_defaultMaterial = GetComponent<Renderer>().material;

	}
	
	// Update is called once per frame
	void Update()
    {}

	void OnCollisionEnter(Collision col){}
	void OnCollisionExit(Collision col){}
	void OnCollisionStay(Collision col){}
    
	public void OnCollisionEnterCor(Colored col)
	{
        print("encostei em alguem colorido " + col.gameObject.name + " com cor " + col.cor.name);
        _coresEmContato.Add(col.cor);
        _corParede.Add(col.corlist);

    }
	public void OnCollisionExitCor(Colored col)
	{
        print("soltei de alguem colorido " + col.gameObject.name);
        _coresEmContato.Remove(col.cor);
        _corParede.Remove(col.corlist);

        //verificar se estou encostado em alguem colorido
        //  se estou muda para a cor
        //  se nao estou volto para a cor default
        //Se nao estava camuflado, entao nao passo a ficar
        // ALTERAR ISSO
        int i = 0;
        foreach (Transform child in model.GetComponentInChildren<Transform>())
        {
            if (!(child.GetComponent<Renderer>().material == _defaultMaterial[i]))
            {
                SetCamufla(true);
            }
            i++;
        }

        // if (GetComponent<Renderer>().material != _defaultMaterial)
        // SetCamufla(true);

        //preciso de uma forma de verificar todos os itens em colisao comigo e todos que sao colored
        //  encontrei uma forma de fazer o segundo mas nao o primeiro. Para fazer manualmente:
        //      posso inserir numa lista todos os itens coloridos e retira-los quando collision exit
        //      dai e so checar essa lista
    }

	public void SetCamufla(bool b)
	{

        /*
         *  Passe b como verdadeiro para camuflar.
         *  Passe b como falso para voltar a cor default
         *  Se b for verdadeiro mas nao houver cor em contato que possa camuflar entao ele volta para a cor default.
        */

		if (b)
		{
			foreach (Material c in _coresEmContato)
			{
				print("procurando " + c.name + " em cores disponiveis");
				if (_coresDisponiveis.Contains(_corParede[0]))
				{
                    // ALTERAR ISSO
                    foreach (Transform child in model.GetComponentInChildren<Transform>())
                    {
                        child.GetComponent<Renderer>().material = c;
                    }
					//GetComponent<Renderer>().material = c;
					gameObject.layer = 12;
                    /*
                     * layer 9 e a do player quando nao esta camuflado
                     * layer 11 e a layer dos enemies
                     * layer 12 e a do player quando ele esta camuflado.
                     * essas duas layers nao colidem uma com a outra
                     * 
                     **/
					return;
				}
			}
		}
		gameObject.layer = 9;
        /*
         * layer 9 e a do player quando nao esta camuflado
         * layer 11 e a layer dos enemies
         * layer 12 e a do player quando ele esta camuflado.
         * essas duas layers nao colidem uma com a outra
         * 
         **/

        // ALTERAR ISSO
        int i = 0;
        foreach (Transform child in model.GetComponentInChildren<Transform>())
        {
            child.GetComponent<Renderer>().material = _defaultMaterial[i];
            i++;
        }
        //GetComponent<Renderer>().material = _defaultMaterial;
	}

	public void AddColor(Colored.Corenum c)
	{
        if (!_coresDisponiveis.Contains(c))
        {
            _coresDisponiveis.Add(c);
        }
	}
	public bool IsCamuflado()
	{
        // alterar aqui para mudar a cor do player quando camuflado
        int i = 0;
        foreach (Transform child in model.GetComponentInChildren<Transform>())
        {
            if (!(child.GetComponent<Renderer>().material == _defaultMaterial[i]))
            {
                return true;
            }
            i++;
        }
        return false;
        //return GetComponent<Renderer>().material != _defaultMaterial; 
	}
}
